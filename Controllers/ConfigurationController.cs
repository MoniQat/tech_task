using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using tech_task.Models;

namespace tech_task.Controllers
{
    public class ConfigurationController : Controller
    {
        public IActionResult Index()
        {
            using (var _context = new ConfigContext())
            {
                var configurations = _context.Configurations
                    .Include(c => c.RootElement)
                    .ToList();

                var rootElements = configurations.Select(c => c.RootElement).ToList();

                var treeNodes = rootElements.Select(root => BuildTree(root, _context)).ToList();

                return View(treeNodes);
            }
        }

        private TreeNodeViewModel BuildTree(ConfigurationItem node, ConfigContext context)
        {
            var treeNode = new TreeNodeViewModel
            {
                Id = node.Id,
                Text = node.ParentId.HasValue
                    ? $"{node.Key}: {node.Value}"
                    : $"{node.Id} ({context.Configurations.FirstOrDefault(c => c.RootElementId == node.Id)?.Comment})",
                Children = GetChildren(node.Id, context)
            };

            return treeNode;
        }

        private List<TreeNodeViewModel> GetChildren(int parentId, ConfigContext context)
        {
            var childNodes = context.ConfigurationItems
                .Where(c => c.ParentId == parentId)
                .ToList();

            var childTreeNodes = childNodes.Select(child => BuildTree(child, context)).ToList();

            return childTreeNodes;
        }


        [HttpPost]
        public IActionResult UploadJson(IFormFile jsonFile, string comment)
        {
            if (jsonFile != null && jsonFile.Length > 0)
            {
                using (var stream = new StreamReader(jsonFile.OpenReadStream()))
                {
                    var jsonString = stream.ReadToEnd();
                    try
                    {
                        CreateConfigurationHierarchy(jsonString, comment);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Exception during processing JSON: {ex.Message}");
                    }
                }
            }

            return RedirectToAction("Index");
        }

        private void CreateConfigurationHierarchy(string jString, string comment, ConfigurationItem? parentItem = null)
        {
            IDictionary<string, JToken> JsonData = JObject.Parse(jString);

            using (var _context = new ConfigContext())
            {
                if (parentItem == null)
                {
                    var rootItem = new ConfigurationItem
                    {
                        Key = "rootItem"
                    };
                    _context.ConfigurationItems.Add(rootItem);
                    _context.SaveChanges();

                    var configuration = new Configuration
                    {
                        Comment = comment,
                        RootElementId = rootItem.Id,
                        RootElement = rootItem
                    };
                    _context.Configurations.Add(configuration);
                    _context.SaveChanges();

                    Console.WriteLine($"Root element created{rootItem.Id}");
                    CreateConfigurationHierarchy(jString, comment, rootItem);

                    return;
                }

                foreach (KeyValuePair<string, JToken> element in JsonData)
                {
                    Console.WriteLine($"Parent element {parentItem.Id}");
                    var configItem = new ConfigurationItem
                    {
                        Key = element.Key,
                        ParentId = parentItem.Id
                    };
                    _context.ConfigurationItems.Add(configItem);
                    _context.SaveChanges();
                    try
                    {
                        Console.WriteLine($"ParentId is {parentItem.Id}");
                        Console.Write($"{element.Key}: ");
                        CreateConfigurationHierarchy(element.Value.ToString(), comment, configItem);
                    }
                    catch
                    {
                        configItem.Value = element.Value.ToString();
                        _context.ConfigurationItems.Update(configItem);
                        _context.SaveChanges();
                        Console.WriteLine($"{element.Value}");
                    }
                    _context.SaveChanges();
                }
            }
        }
    }
}
