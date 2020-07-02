namespace Reinforced.Typings.Core.Settings
{
    public class ReinforcedTypingsSettings
    {

        /// <summary>
        /// Target file where to store generated sources.
        /// This parameter is not used when Hierarchy is true
        /// </summary>
        public string TargetFile { get; set; }

        /// <summary>
        /// Target directory where to store generated typing files.
        /// This parameter is not used when Hierarchy is false
        /// </summary>
        public string TargetDirectory { get; set; }

        /// <summary>
        /// True to create project hierarchy in target folder.
        /// False to store generated typings in single file
        /// </summary>
        public bool Hierarchy { get; set; }

        /// <summary>
        /// Full path to assembly's XMLDOC file.
        /// If this parameter is not specified or contains invalid path then documentation will not be generated without any exception
        /// </summary>
        public string DocumentationFilePath { get; set; }
    }
}
