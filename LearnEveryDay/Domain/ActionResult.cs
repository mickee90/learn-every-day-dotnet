using System.Collections.Generic;

namespace LearnEveryDay.Domain
{
    public class ActionResult
    {
        public ActionResult()
        {

        }

        public bool Success { get; set; }

        public IEnumerable<string> Errors { get; set; }
    }
}