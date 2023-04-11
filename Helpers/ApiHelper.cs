namespace BackEndTestTask.Helpers
{
    public static class ApiHelper
    {
        private const string DefaultGenericRoot = "api.user.";

        #region Tree endpoints
        private const string DefaultTreeRoot = DefaultGenericRoot + "tree.";

        public const string RootGet = DefaultTreeRoot + "get";
        #endregion
        
        #region Node endpoints
        private const string DefaultNodeRoot = DefaultTreeRoot + "node.";

        public const string NodeCreate = DefaultNodeRoot + "create";
        public const string NodeDelete = DefaultNodeRoot + "delete";
        public const string NodeRename = DefaultNodeRoot + "rename";
        #endregion

        #region Journal endpoints
        private const string DefaultJournalRoot = DefaultGenericRoot + "journal.";

        public const string JournalGetRange = DefaultJournalRoot + "getRange";
        public const string JournalGetSingle = DefaultJournalRoot + "getSingle";
        #endregion
    }
}