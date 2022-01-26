using System;
using System.Reflection;
using System.IO;

namespace CDR.Decoder
{
    public class AssemblyVersionInfo
    {
        private string _Title;
        private string _Copyright;
        private Version _Version;
        private DateTime _lastBuildDate;

        public string Title { get { return _Title; } }
        public string Copyright { get { return _Copyright; } }
        public Version Version { get { return _Version; } }
        public DateTime LastBuildDate { get { return _lastBuildDate; } }

        public AssemblyVersionInfo(Type type)
        {
            Assembly asm = Assembly.GetAssembly(type);
            object[] atts = asm.GetCustomAttributes(false);
            FileStream[] asmFiles = asm.GetFiles();

            DateTime dt;
            _lastBuildDate = DateTime.MinValue;

            foreach (FileStream file in asmFiles)
            {
                dt = File.GetLastWriteTime(file.Name);
                if (dt > _lastBuildDate)
                    _lastBuildDate = dt;
            }

            _Version = asm.GetName().Version;
            
            for (int n = 0; n < atts.Length; n++)
            {
                if (atts[n] is AssemblyTitleAttribute)
                {
                    _Title = ((AssemblyTitleAttribute)atts[n]).Title;
                }
                else if (atts[n] is AssemblyCopyrightAttribute)
                {
                    _Copyright = ((AssemblyCopyrightAttribute)atts[n]).Copyright;
                }
            }
        }
    }
}