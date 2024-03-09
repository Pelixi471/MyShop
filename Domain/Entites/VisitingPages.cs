using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Domain.Entites
{
    public class VisitingPages : IEntity
    {
        public int Id { get; init; }
        private string _path = null!;
        private int _numberOfClicks;
        public VisitingPages(string path)
        {
            Path = path;
            NumberOfClicks = 1;
        }

        public string Path 
        { 
            get => _path; 
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("path can't be empty", nameof(value));
                _path = value;
            }
        }

        public int NumberOfClicks
        {
            get => _numberOfClicks;
            set
            {
                _numberOfClicks = value;
            }
        }

        
    }
}
