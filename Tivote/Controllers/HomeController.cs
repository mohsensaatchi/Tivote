using System;
using System.Diagnostics;
using System.Globalization;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Tivote.Data;
using Tivote.Models;
using Tivote.Models.Admin;
using Tivote.Models.News;
using Tivote.Models.ViewModels;

namespace Tivote.Controllers;

[Authorize()]
public class HomeController : MainController
{
    public HomeController(TivoteDb context) : base(context)
    {
    }
    public async Task<IActionResult> Index()
    {
        User? currentUser = await CurrentUserAsync();
        IEnumerable<NewsCategory>? newsCategories = await Context.NewsCategories.Include(nc => nc.News).ToListAsync();
        if (newsCategories is null)
        {
            return NotFound();
        }
        return View(newsCategories);
    }
    public async Task<IActionResult> Content(Guid id)
    {
        News? news = await Context.News.Include(n => n.Category).FirstOrDefaultAsync(n => n.Id == id);
        if (news is null)
        {
            return NotFound();
        }
        ViewData["News"] = news;
        IEnumerable<NewsCategory>? newsCategories = await Context.NewsCategories.Include(nc => nc.News).ToListAsync();
        return View(newsCategories);
    }
    public async Task<IActionResult> CreateNews()
    {
        IEnumerable<NewsCategory> newsCategories = await Context.NewsCategories.ToListAsync();
        SelectListItem[] newsCategoryItems = new SelectListItem[newsCategories.Count()];
        for (int i = 0; i < newsCategories.Count(); i++)
        {
            newsCategoryItems[i] = new SelectListItem { Value = newsCategories.ElementAt(i).Id.ToString(), Text = newsCategories.ElementAt(i).Name };
        }
        ViewData["Categories"] = newsCategoryItems;
        return PartialView();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateNews(NewsDto newsDto)
    {
        if (ModelState.IsValid)
        {
            string uniqueFileName = ProcessUploadedFile(newsDto);
            News news = new()
            {
                Id = Guid.NewGuid(),
                Title = newsDto.Title,
                Description = newsDto.Description,
                Content = newsDto.Content,
                ImageUrl = Path.Combine(uniqueFileName)
            };
            NewsCategory? newsCategory = Context.NewsCategories.Find(newsDto.CategoryId);
            if (newsCategory is null) return BadRequest();
            news.Category = newsCategory;
            Context.Add(news);
            await Context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return PartialView(newsDto);
    }
    public IActionResult CreateNewsCategory()
    {
        return PartialView();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateNewsCategory([Bind("Name")] NewsCategory newsCategory)
    {
        if (ModelState.IsValid)
        {
            newsCategory.Id = Guid.NewGuid();
            Context.Add(newsCategory);
            await Context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return PartialView("Index",newsCategory);
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    public async Task<IActionResult> News(Guid id)
    {
        News? news = await Context.News.FindAsync(id);
        if (news is null)
        {
            return NotFound();
        }
        return View(news);
    }
    public void alaki()
    {
        News[] news = new News[5];
        for (int i = 0; i < 5; i++)
        {
            news[i] = new News();
            news[i].Title = "عنوان خبر " + i;
            news[i].Description = "توضیح کوتاه خبر " + i;
            news[i].ImageUrl = $"~/images/{i}.png";
            news[i].Content = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است. چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است و برای شرایط فعلی تکنولوژی مورد نیاز و کاربردهای متنوع با هدف بهبود ابزارهای کاربردی می باشد. کتابهای زیادی در شصت و سه درصد گذشته، حال و آینده شناخت فراوان جامعه و متخصصان را می طلبد تا با نرم افزارها شناخت بیشتری را برای طراحان رایانه ای علی الخصوص طراحان خلاقی و فرهنگ پیشرو در زبان فارسی ایجاد کرد. در این صورت می توان امید داشت که تمام و دشواری موجود در ارائه راهکارها و شرایط سخت تایپ به پایان رسد وزمان مورد نیاز شامل حروفچینی دستاوردهای اصلی و جوابگوی سوالات پیوسته اهل دنیای موجود طراحی اساسا مورد استفاده قرار گیرد.";
        }
        Context.News.AddRange(news);
        Context.SaveChanges();
    }
    private static string ProcessUploadedFile(NewsDto model)
    {
        string filePath = "";
        string result = "";
        string path = Path.Combine("wwwroot", "Uploads");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        if (model.Image != null)
        {
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
            filePath = Path.Combine("wwwroot", "Uploads", uniqueFileName);
            result = Path.Combine("Uploads", uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                model.Image.CopyTo(fileStream);
            }
        }
        return result;
    }
    public void Allaki()
    {
        MultipleChoiceSurvey multipleChoiceSurvey = new();
        multipleChoiceSurvey.Id = Guid.NewGuid();
        multipleChoiceSurvey.Question = "با سلام و احترام<br/>در راستای ارائه خدمات مطلوب تر و کسب رضایت حداکثری شما همکاران محترم، خواهشمند است با شرکت در نظرسنجی زیر ما را در ارائه خدمات هرچه بهتر یاری رسانید.<br/>کدام یک از کترینگ های ذیل عملکرد مناسب تری از نظرکیفیت، کمیت، تنوع و نحوه بسته بندی را کسب نموده است؟<br/>";
        multipleChoiceSurvey.ImageUrl = "Uploads/nazarsanji.jpg";
        Choice[] choices = new Choice[4];
        choices[0] = new Choice() { Id = Guid.NewGuid(), Text = "فارسی" };
        choices[0].ImageUrl = "Uploads/farsi.jpg";
        choices[1] = new Choice() { Id = Guid.NewGuid(), Text = "قریشی" };
        choices[1].ImageUrl = "Uploads/qoraishi.png";
        choices[2] = new Choice() { Id = Guid.NewGuid(), Text = "ناریجه" };
        choices[2].ImageUrl = "Uploads/narijeh.png";
        choices[3] = new Choice() { Id = Guid.NewGuid(), Text = "دونا" };
        choices[3].ImageUrl = "Uploads/dona.png";
        multipleChoiceSurvey.Choices.AddRange(choices);
        Context.Choices.AddRange(choices);
        Context.MultipleChoiceSurveys.Add(multipleChoiceSurvey);
        Context.SaveChanges();
    }
    public void palaki()
    {
        Title title = new();
        title.Id = Guid.NewGuid();
        title.Name = "آقای";
        Context.Titles.Add(title);
        title = new();
        title.Id = Guid.NewGuid();
        title.Name = "خانم";
        Context.Titles.Add(title);
        Context.SaveChanges();
    }

    public async Task<IActionResult> Survey(Guid id)
    {
        VoteDto voteDto = new();
        if (id == Guid.Empty) id = (await Context.MultipleChoiceSurveys.FirstAsync()).Id;
        MultipleChoiceSurvey? survey = await Context.MultipleChoiceSurveys.FindAsync(id);
        _ = await Context.Choices.ToListAsync();
        if (survey is null)
            return NotFound();
        voteDto.MultipleChoiceSurveyId = survey.Id;
        User? user = await CurrentUserAsync();
        if (user is null)
            return NotFound();
        Vote? vote = await Context.Votes.Where(v => v.User.Id == user.Id && v.MultipleChoiceSurvey.Id == id).FirstOrDefaultAsync();
        if (vote is not null)
        {
            return View("SurveyEnd", vote.SelectedChoice.Text);
        }
        voteDto.UserId = user.Id;
        voteDto.MultipleChoiceSurveyId = survey.Id;
        voteDto.Question = survey.Question;
        voteDto.Choices = survey.Choices;
        voteDto.ImageUrl = survey.ImageUrl;
        return View(voteDto);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Survey([Bind("UserId,MultipleChoiceSurveyId,SelectedChoice,Id")] VoteDto voteDto)
    {
        Vote vote = new();
        vote = new();
        if (voteDto.SelectedChoice != Guid.Empty)
        {
            MultipleChoiceSurvey? survey = await Context.MultipleChoiceSurveys.FindAsync(voteDto.MultipleChoiceSurveyId);
            Choice? choice = await Context.Choices.FindAsync(voteDto.SelectedChoice);
            User? user = await CurrentUserAsync();
            if (survey is null || choice is null || user is null)
                return NotFound();
            vote.MultipleChoiceSurvey = survey;
            vote.SelectedChoice = choice;
            vote.User = user;
            Context.Votes.Add(vote);
            Context.SaveChanges();
        }
        return RedirectToAction("Survey", voteDto.MultipleChoiceSurveyId);
    }
    public IActionResult SurveyEnd(string choice)
    {
        return View(choice);
    }
}