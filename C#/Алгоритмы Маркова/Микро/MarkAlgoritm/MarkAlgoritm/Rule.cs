namespace MarkAlgoritm
{
    class Rule
    {
        public Rule() { }
        
        public Rule(string first, string second)
        {
            First = first;
            Second = second;
        }

        public string First { get; set; }

        public string Second { get; set; }
    }
}
