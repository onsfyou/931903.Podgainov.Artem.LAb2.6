using Microsoft.AspNetCore.Mvc;
using LAb6.Models;
using LAb6.Data;
using Microsoft.EntityFrameworkCore;

namespace LAb6.Controllers
{
    public class MockupsController : Controller
    {
        private readonly ApplicationDbContext _context;
        IWebHostEnvironment _appEnvironment;
        public MockupsController(ApplicationDbContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        public async Task<IActionResult> AllForums()
        {
            return _context.Categories != null ?
                        View(await _context.Categories.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Forums'  is null.");
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SingleForum()
        {
            var firstForum = _context.Categories.First();

            ViewBag.ForumName = firstForum.Name;

            var forumTopics = await _context.Topics.Where(rec => rec.ParentForumId == firstForum.Id).ToListAsync();

            var replies = _context.Replies;

            ViewBag.TopicsRepliesAmount = new Dictionary<int, int>();
            ViewBag.TopicsReplies = new Dictionary<int, Reply[]>();

            foreach (var topic in forumTopics)
            {
                var tempReplies = await replies.Where(rec => rec.ParentTopicId == topic.Id).ToListAsync();

                ViewBag.TopicsRepliesAmount[topic.Id] = tempReplies.Count;
                ViewBag.TopicsReplies[topic.Id] = new Reply[2];
                ViewBag.TopicsReplies[topic.Id][0] = tempReplies.OrderBy(x => x.CreatedDate).FirstOrDefault();
                ViewBag.TopicsReplies[topic.Id][1] = tempReplies.OrderBy(x => x.CreatedDate).LastOrDefault();
            }

            return View(forumTopics);
        }

        public async Task<IActionResult> SingleTopic()
        {
            var firstTopic = _context.Topics.First();
            var parentForum = _context.Categories.Where(x => x.Id == firstTopic.ParentForumId).First();
            ViewBag.ParentTopic = firstTopic;
            ViewBag.ParentForum = parentForum;

            var topicReplies = await _context.Replies.Where(rec => rec.ParentTopicId == firstTopic.Id).ToListAsync();

            ViewBag.RepliesAttachedFiles = new Dictionary<int, List<UseFile>>();

            var files = _context.UseFiles;

            foreach (var reply in topicReplies)
            {
                var tempFiles = await files.Where(rec => rec.ParentReplyId == reply.Id).ToListAsync();
                ViewBag.RepliesAttachedFiles[reply.Id] = tempFiles;
            }

            return View(topicReplies);
        }

        [HttpPost]
        public async Task<IActionResult> AddFile(IFormFile uploadedFile, int replyId)
        {
            if (uploadedFile != null)
            {
                string path = "/Files/" + uploadedFile.FileName;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                UseFile file = new UseFile { Name = uploadedFile.FileName, Path = path, ParentReplyId = replyId };
                _context.UseFiles.Add(file);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

    }
}
