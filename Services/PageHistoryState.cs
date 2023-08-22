namespace Recipi_PWA
{
    public class PageHistoryState
    {
        private List<string> previousPages;

        public PageHistoryState()
        {
            previousPages = new List<string>();
        }
        public void AddPageToHistory(string pageName)
        {
            previousPages.Add(pageName);
        }

        public string GetGoBackPage()
        {
            if (previousPages.Count > 1)
            {
                // You add a page on initialization, so you need to return the 2nd from the last
                return previousPages.ElementAt(previousPages.Count - 2);
            }

            // Can't go back because you didn't navigate enough
            return previousPages.FirstOrDefault();
        }

        public bool CanGoBack()
        {
            return previousPages.Count > 1;
        }
    }
}