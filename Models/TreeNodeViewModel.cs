﻿namespace tech_task.Models
{
    public class TreeNodeViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Value { get; set; }
        public List<TreeNodeViewModel> Children { get; set; }
    }
}
