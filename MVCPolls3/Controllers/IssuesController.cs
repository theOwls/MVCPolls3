using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCPolls3.Data;
using MVCPolls3.Models;

namespace MVCPolls3.Controllers
{
    public class IssuesController : Controller
    {
        private readonly PollsContext _context;

        public IssuesController(PollsContext context)
        {
            _context = context;    
        }

        // GET: Issues
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewData["VoteSortParm"] = String.IsNullOrEmpty(sortOrder) ? "vote_asc" : "";
            ViewData["PostCodeSortParm"] = sortOrder == "Postcode" ? "post_desc" : "Postcode";
            var issues = from i in _context.Issues
                           select i;
            switch (sortOrder)
            {
                case "vote_asc":
                    issues = issues.OrderBy(i => i.Votes);
                    break;
                case "Postcode":
                    issues = issues.OrderBy(i => i.Postcode);
                    break;
                case "post_desc":
                    issues = issues.OrderByDescending(i => i.Postcode);
                    break;
                default:
                    issues = issues.OrderByDescending(s => s.Votes);
                    break;
            }
            return View(await issues.AsNoTracking().ToListAsync());
        }

        // GET: Issues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = await _context.Issues
                .SingleOrDefaultAsync(m => m.ID == id);
            if (issue == null)
            {
                return NotFound();
            }

            return View(issue);
        }

        // GET: Issues/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Issues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Description,Votes,Postcode")] Issue issue)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(issue);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }

            return View(issue);
        }

        // GET: Issues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = await _context.Issues.SingleOrDefaultAsync(m => m.ID == id);
            if (issue == null)
            {
                return NotFound();
            }
            return View(issue);
        }

        // POST: Issues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Description,Votes,Postcode")] Issue issue)
        {
            if (id != issue.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(issue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IssueExists(issue.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(issue);
        }

        // GET: Issues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = await _context.Issues
                .SingleOrDefaultAsync(m => m.ID == id);
            if (issue == null)
            {
                return NotFound();
            }

            return View(issue);
        }

        // POST: Issues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var issue = await _context.Issues.SingleOrDefaultAsync(m => m.ID == id);
            _context.Issues.Remove(issue);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool IssueExists(int id)
        {
            return _context.Issues.Any(e => e.ID == id);
        }
    }
}
