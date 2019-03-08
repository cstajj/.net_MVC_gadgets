using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class JsonTree
    {
        private int _id;
        private string _text;
        private string _state = "open";
        private Dictionary<string, string> _attributes = new Dictionary<string, string>();
        private object _children;
        private int _score;

        public int id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string text
        {
            get { return _text; }
            set { _text = value; }
        }
        public string state
        {
            get { return _state; }
            set { _state = value; }
        }
        public Dictionary<string, string> attributes
        {
            get { return _attributes; }
            set { _attributes = value; }
        }
        public object children
        {
            get { return _children; }
            set { _children = value; }
        }
        public int score {
            get { return _score; }
            set { _score = value; }
        }
    }
}

