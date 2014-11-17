namespace CrudeNews.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;
    using CrudeNews.Data;
    using CrudeNews.Web.Models.Articles;
    using CrudeNews.Models;
    using System.Net;
    using System;
    using System.IO;
    using CrudeNews.Helpers;
    using System.Text;
    using System.Collections.Generic;
    using PagedList;

    public class ArticlesController : Controller
    {
        public ICrudeNewsData Data { get; set; }
        private CreateArticleViewModel DefaultModel { get; set; }

        public ArticlesController(ICrudeNewsData data)
        {
            this.Data = data;
            this.DefaultModel = new CreateArticleViewModel
            {
                CategoryNames = this.Data.Categories.All().Select(x => x.Name)
            };
        }

        public ArticlesController()
            : this(new CrudeNewsData())
        {
        }

        public ActionResult All(int? page)
        {
            var listPaged = GetPagedNames(page); // GetPagedNames is found in BaseController
            if (listPaged == null)
                return HttpNotFound();
            var model = new AllArticlesViewModel
            {
                Articles = listPaged,
                Page = 2
            };

            return Request.IsAjaxRequest()
                ? (ActionResult)PartialView("_Pages", model)
                : View(model);
        }

        protected IPagedList<ArticleSmallViewModel> GetPagedNames(int? page)
        {
            // return a 404 if user browses to before the first page
            if (page.HasValue && page < 1)
                return null;

            // retrieve list from database/whereverand
            var listUnpaged = this.Data.Articles
                .All()
                .Project()
                .To<ArticleSmallViewModel>();

            // page the list
            const int pageSize = 10;
            var listPaged = listUnpaged.OrderBy(x => x.DateCreated).ToPagedList(page ?? 1, pageSize);

            // return a 404 if user browses to pages beyond last page. special case first page if no items exist
            if (listPaged.PageNumber != 1 && page.HasValue && page > listPaged.PageCount)
                return null;

            return listPaged;
        }

        public ActionResult Show(int id)
        {
            var model = this.Data.Articles
                .All()
                .Where(x => x.ID == id)
                .Project()
                .To<ArticleViewModel>()
                .FirstOrDefault();

            return View(model);
        }

        //[Authorize(Roles="Admin")]
        public ActionResult Create()
        {
            return View(this.DefaultModel);
        }

        //[Authorize(Roles="Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateArticleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.CategoryNames = this.DefaultModel.CategoryNames;
                return View(model);
            }

            var author = this.Data.Users.All()
                .FirstOrDefault(x => x.UserName == this.User.Identity.Name);
            var category = this.Data.Categories.All()
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

            this.Data.Articles.Add(article);
            this.Data.SaveChanges();

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

                    var tag = this.Data.Tags
                        .All()
                        .FirstOrDefault(x => x.Name == tagName.ToString());

                    if (tag == null)
                    {
                        tag = new Tag
                        {
                            Name = tagName
                        };

                        this.Data.Tags.Add(tag);
                        this.Data.SaveChanges();
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