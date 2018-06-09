using System;

namespace Domain
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        private Category()
        {
        }

        public Category(string title)
        {
            if (String.IsNullOrEmpty(title))
            {
                throw new ArgumentNullException("Title must not be null");
            }
            
            Title = title;
        }
        public override string ToString()
        {
            return Title;
        }
    }
}