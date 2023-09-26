namespace backendAPIs.Util
{
    public class UIDGenerator
    {
        public static string GenerateUniqueVarcharId(string prefix)
        {
            string uniqueId = Guid.NewGuid().ToString();
            return prefix + "-" + uniqueId.Substring(0,5);
        }
    }
}
