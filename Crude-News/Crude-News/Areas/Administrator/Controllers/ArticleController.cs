namespace CrudeNews.Web.Areas.Administrator.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web.Mvc;

    using CrudeNews.Data;
    using CrudeNews.Models;
    using CrudeNews.Web.Areas.Administrator.Models;

    [Authorize]
    public class ArticleController : Controller
    {
        private CreateArticleViewModel DefaultModel { get; set; }
        public readonly ICrudeNewsData data;

        public ArticleController(ICrudeNewsData data)
        {
            this.data = data;
            this.DefaultModel = new CreateArticleViewModel
            {
                CategoryNames = this.data.Categories.All().Select(x => x.Name)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateArticleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.CategoryNames = this.DefaultModel.CategoryNames;
                return View(model);
            }

            var author = this.data.Users.All()
                .FirstOrDefault(x => x.UserName == this.User.Identity.Name);
            var category = this.data.Categories.All()
                .FirstOrDefault(x => x.Name == model.Category);
            var content = WebUtility.HtmlEncode(model.Content);
            var title = WebUtility.HtmlEncode(model.Title);
            IList<Tag> tags = new List<Tag>();
            string path;

            if (model.Tags != null)
            {
                tags = this.GetTags(model.Tags);
            }

            if (model.Image != null)
            {
                string folderLocation = "~/Content/ArticleImages";
                string fileExtension = Path.GetExtension(model.Image.FileName);
                string fileName = Path.GetFileNameWithoutExtension(model.Image.FileName);
                string fullName = fileName + "_" + GetRandomString(16) + fileExtension;
                path = Path.Combine(
                    Server.MapPath(folderLocation), fullName);
                model.Image.SaveAs(path);
            }
            else
            {
                path = "~/Content/Images/cat.jpg";
            }

            var article = new Article
            {
                AuthorID = author.Id,
                CategoryID = category.ID,
                Content = content,
                DateCreated = DateTime.Now,
                Title = title,
                ImagePath = path,
                Tags = tags
            };

            this.data.Articles.Add(article);
            this.data.SaveChanges();

            return View(this.DefaultModel);
        }

        public ActionResult Create()
        {
            return View(this.DefaultModel);
        }     

        private IList<Tag> GetTags(string tags)
        {
            IList<Tag> allTags = new List<Tag>();
            StringBuilder currentTag = new StringBuilder();
            string trimmedTagsString = tags.Trim();

            char letter;
            for (int i = 0; i < trimmedTagsString.Length; i++)
            {
                letter = tags[i];

                if (letter == '[')
                {
                    i++;
                    while (i < trimmedTagsString.Length)
                    {
                        letter = tags[i];
                        if (letter == ']')
                        {
                            break;
                        }

                        currentTag.Append(letter);
                        i++;
                    }

                    string tagName = currentTag.ToString();
                    currentTag.Clear();

                    var tag = this.data.Tags
                        .All()
                        .FirstOrDefault(x => x.Name == tagName.ToString());

                    if (tag == null)
                    {
                        tag = new Tag
                        {
                            Name = tagName
                        };

                        this.data.Tags.Add(tag);
                        this.data.SaveChanges();
                    }

                    allTags.Add(tag);
                }
            }

            return allTags;
        }

        private string GetRandomString(int size)
        {
            Random random = new Random();
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }
    }
}