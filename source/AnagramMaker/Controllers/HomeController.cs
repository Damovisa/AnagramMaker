using System.Web.Mvc;
using AnagramMaker.Lib;

namespace AnagramMaker.Controllers
{
    public class HomeController : Controller
    {
        private AnagramCreator _anagramLib;

        public HomeController()
        {
            _anagramLib = new AnagramCreator();
        }

        // GET: Home
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetAnagrams(string word)
        {
            var anagrams = _anagramLib.GenerateAnagrams(word);
            return Json(new { items = anagrams }, JsonRequestBehavior.AllowGet);
        }
    }
}