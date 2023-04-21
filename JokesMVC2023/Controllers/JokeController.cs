using JokesMVC2023.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JokesMVC2023.Models.Data;

namespace JokesMVC2023.Controllers
{
    public class JokeController : Controller
    {
        private readonly JokeDBContext _jokeContext;
        public JokeController(JokeDBContext jokeContext)
        {
            _jokeContext = jokeContext;
        }

        // GET: JokeController
        public ActionResult Index()
        {
            return View(_jokeContext.Jokes.AsEnumerable());
        }

        [HttpGet]
        public async Task<ActionResult> JokeTablePartial()
        {
            return PartialView("_JokeTable", _jokeContext.Jokes.AsEnumerable());
        }

        //[HttpPost]
        //public ActionResult Search(IFormCollection formCollection)
        //{
        //    var jokes = _jokeContext.Jokes.AsQueryable();

        //    var question = formCollection["questionSearch"].ToString();
        //    var answer = formCollection["answerSearch"].ToString();

        //    if (!String.IsNullOrEmpty(question))
        //    {
        //        jokes = jokes.Where(c => c.JokeQuestion.Contains(question));
        //    }

        //    if (!String.IsNullOrEmpty(answer))
        //    {
        //        jokes = jokes.Where(c => c.JokeAnswer.Contains(answer));
        //    }

        //    var jokesResult = jokes.ToList();
        //    return View("PublicJokes", jokesResult);

        //}

        //// GET: JokeController/Details/5
        //public ActionResult Details(int id)
        //{
        //    if(id == 0)
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }

        //    var joke = _jokeContext.Jokes.FirstOrDefault(c => c.Id == id);

        //    return joke != null ? View(joke) : RedirectToAction(nameof(Index));
        //}

        // GET: JokeController/Details/5
        [HttpGet]
        public async Task<ActionResult> Details([FromRoute]int id)
        {
            if (id == 0)
            {
                return RedirectToAction(nameof(Index));
            }

            var joke = _jokeContext.Jokes.FirstOrDefault(c => c.Id == id);

            return joke != null ? PartialView("_Details", joke) : RedirectToAction(nameof(Index));
        }

        // GET: JokeController/Create
        public async Task<ActionResult> Create()
        {
            return PartialView("_Create");
        }

        // POST: JokeController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([FromBody]JokeCreateDTO jokeCreate)
        {
            try
            {
                // simple error handling
                if (ModelState.IsValid)
                {
                    _jokeContext.Jokes.Add(new Joke { JokeQuestion = jokeCreate.JokeQuestion, JokeAnswer = jokeCreate.JokeAnswer });
                    _jokeContext.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        //// GET: JokeController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: JokeController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(JokeCreateDTO jokeCreate)
        //{
        //    try
        //    {
        //        // simple error handling
        //        if (ModelState.IsValid)
        //        {
        //            _jokeContext.Jokes.Add(new Joke { JokeQuestion = jokeCreate.JokeQuestion, JokeAnswer = jokeCreate.JokeAnswer});
        //            _jokeContext.SaveChanges();
        //            return RedirectToAction(nameof(Index));
        //        }
        //        else
        //        {
        //            return View();
        //        }         
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}


        //// GET: JokeController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    if (id == 0)
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }

        //    var joke = _jokeContext.Jokes.FirstOrDefault(c => c.Id == id);

        //    return joke != null ? View(joke) : RedirectToAction(nameof(Index));
        //}

        //// POST: JokeController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, Joke joke)
        //{
        //    if (id != joke.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        _jokeContext.Jokes.Update(joke);

        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(joke);
        //}


        // GET: JokeController/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit([FromRoute]int id)
        {
            if (id == 0)
            {
                return RedirectToAction(nameof(Index));
            }

            var joke = _jokeContext.Jokes.FirstOrDefault(c => c.Id == id);

            return joke != null ? PartialView("_Edit", joke) : RedirectToAction(nameof(Index));
        }

        // POST: JokeController/Edit/5
        [HttpPut]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([FromRoute]int id, [FromBody]Joke joke)
        {
            if (id != joke.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _jokeContext.Jokes.Update(joke);
                _jokeContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(joke);
        }



        //// GET: JokeController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    if (id == 0)
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }

        //    var joke = _jokeContext.Jokes.FirstOrDefault(c => c.Id == id);

        //    return joke != null ? View(joke) : RedirectToAction(nameof(Index));
        //}

        //// POST: JokeController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        var joke = _jokeContext.Jokes.FirstOrDefault(c => c.Id == id);
        //        if (joke != null)
        //        {
        //            _jokeContext.Jokes.Remove(joke);
        //            _jokeContext.SaveChanges();
        //            return RedirectToAction(nameof(Index));
        //        }
        //        return View();
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: JokeController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return RedirectToAction(nameof(Index));
            }

            var joke = _jokeContext.Jokes.FirstOrDefault(c => c.Id == id);

            return joke != null ? View(joke) : RedirectToAction(nameof(Index));
        }

        // POST: JokeController/Delete/5
        [HttpDelete]
        public ActionResult Delete([FromBody]int id, IFormCollection collection)
        {
            try
            {
                var joke = _jokeContext.Jokes.FirstOrDefault(c => c.Id == id);
                if (joke != null)
                {
                    _jokeContext.Jokes.Remove(joke);
                    _jokeContext.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}
