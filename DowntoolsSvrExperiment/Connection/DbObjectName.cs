using System.Text.RegularExpressions;

namespace DowntoolsSvrExperiment.Connection
{
    public class DbObjectName
    {
        // {1,128} - the name is between 1 and 128 chars long
        // Identifiers - http://msdn.microsoft.com/en-us/library/ms175874.aspx
        // Delimited Identifiers - http://msdn.microsoft.com/en-us/library/ms176027.aspx
        // In addition to the characters mentioned in the above two articles, we also 
        // accept '[', ']', '/', '*', ':', '?', '|', '<', and '>'. It is unclear why 
        // these characters should be allowed, maybe it's because we are interested only
        // in database names, whereas the articles refer to all object types.
        private static readonly Regex s_ValidDbDelimitedIdentifierCharacters = new Regex(@"^[\p{L}\p{N}@$#_ ~\-!{%}^'&.(\\)`\[\]/*:?|<>]{1,128}$");

        private readonly string m_Name;

        public DbObjectName(string name)
        {
            m_Name = name;
        }

        public int Length { get { return m_Name.Length; } }

        public DbObjectName Substring(int startIndex, int length)
        {
            return new DbObjectName(m_Name.Substring(startIndex, length));
        }

        public static DbObjectName operator +(DbObjectName n1, object s)
        {
            return new DbObjectName(n1.m_Name + s);
        }

        public static implicit operator string(DbObjectName n)
        {
            return n.ToString();
        }

        public static bool IsValid(string name)
        {
            return s_ValidDbDelimitedIdentifierCharacters.IsMatch(name);
        }

        public override string ToString()
        {
            return Escape();
        }

        public string Escape()
        {
            return "[" + m_Name.Replace("]", "]]") + "]";
        }

        public string Unwrap()
        {
            return m_Name;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return m_Name.Equals(((DbObjectName)obj).m_Name);

        }

        public override int GetHashCode()
        {
            return m_Name.GetHashCode();
        }
    }
}
