using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EasyRent.Data;
using EasyRent.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using EasyRent.ViewModels;

namespace EasyRent.Controllers
{
    [Authorize]
    public class MessagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MessagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Messages
        public async Task<IActionResult> Index(string Id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.NavigatedTO = "Message";
            var applicationDbContext = _context.Messages.Include(m => m.Reciever).Include(m => m.Sender).Where(m => m.RecieverId == userId || m.SenderId == userId);

            try
            {
                if (string.IsNullOrEmpty(Id))
                {
                    var LastMessage = applicationDbContext.OrderByDescending(m => m.Id).FirstOrDefault();
                    if (LastMessage.RecieverId == userId && LastMessage != null)
                    {
                        Id = LastMessage.SenderId;

                    }
                    else if (LastMessage.SenderId == userId && LastMessage != null)
                    {


                        Id = LastMessage.RecieverId;
                    }
                }

            }
            catch
            {

            }


            MessageViewModel messageViewModel = new MessageViewModel()
            {
                AllMessages = await applicationDbContext.ToListAsync(),
                ActiveMessage = _context.Messages.Where(m => m.RecieverId == Id || m.SenderId == Id),
                ActiveUserId = userId,
                RecieverId = Id

            };


            return View(messageViewModel);
        }

        // GET: Messages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.NavigatedTO = "Message";
            if (id == null)
            {
                return NotFound();
            }

            var messages = await _context.Messages
                .Include(m => m.Reciever)
                .Include(m => m.Sender)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (messages == null)
            {
                return NotFound();
            }

            return View(messages);
        }

        // GET: Messages/Create
        public IActionResult Create()
        {
            ViewBag.NavigatedTO = "Message";
            ViewData["RecieverId"] = new SelectList(_context.Users, "Id", "UserName");
            ViewData["SenderId"] = new SelectList(_context.Users, "Id", "UserName");
            return View();
        }

        // POST: Messages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Content,Subject,AttachmentName,SenderId,RecieverId")] Messages messages)
        {
            ViewBag.NavigatedTO = "Message";
            if (ModelState.IsValid)
            {
                _context.Add(messages);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RecieverId"] = new SelectList(_context.Users, "Id", "UserName", messages.RecieverId);
            ViewData["SenderId"] = new SelectList(_context.Users, "Id", "UserName", messages.SenderId);
            return View(messages);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Creates(MessageViewModel messageViewModel)
        {
            ViewBag.NavigatedTO = "Message";
            messageViewModel.DateSent = DateTime.UtcNow;



            if (ModelState.IsValid)
            {
                Messages messages = new Messages()
                {
                    DateSent = DateTime.UtcNow,
                    RecieverId = messageViewModel.RecieverId,
                    SenderId = messageViewModel.ActiveUserId,
                    Content = messageViewModel.Content,
                    Subject = messageViewModel.Subject
                };
                _context.Add(messages);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            string Id = messageViewModel.RecieverId;
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.NavigatedTO = "Message";
            var applicationDbContext = _context.Messages.Include(m => m.Reciever).Include(m => m.Sender).Where(m => m.RecieverId == userId || m.SenderId == userId);

            try
            {
                if (string.IsNullOrEmpty(Id))
                {
                    var LastMessage = applicationDbContext.OrderByDescending(m => m.Id).FirstOrDefault();
                    if (LastMessage.RecieverId == userId && LastMessage != null)
                    {
                        Id = LastMessage.SenderId;

                    }
                    else if (LastMessage.SenderId == userId && LastMessage != null)
                    {


                        Id = LastMessage.RecieverId;
                    }
                }

            }
            catch
            {

            }



            messageViewModel.AllMessages = await applicationDbContext.ToListAsync();
            messageViewModel.ActiveMessage = _context.Messages.Where(m => m.RecieverId == Id || m.SenderId == Id);





            return View("Index", messageViewModel);
        }

        // GET: Messages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.NavigatedTO = "Message";
            if (id == null)
            {
                return NotFound();
            }

            var messages = await _context.Messages.FindAsync(id);
            if (messages == null)
            {
                return NotFound();
            }
            ViewData["RecieverId"] = new SelectList(_context.Users, "Id", "UserName", messages.RecieverId);
            ViewData["SenderId"] = new SelectList(_context.Users, "Id", "UserName", messages.SenderId);
            return View(messages);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Content,Subject,AttachmentName,SenderId,RecieverId")] Messages messages)
        {
            ViewBag.NavigatedTO = "Message";
            if (id != messages.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(messages);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessagesExists(messages.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RecieverId"] = new SelectList(_context.Users, "Id", "UserName", messages.RecieverId);
            ViewData["SenderId"] = new SelectList(_context.Users, "Id", "UserName", messages.SenderId);
            return View(messages);
        }

        // GET: Messages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.NavigatedTO = "Message";
            if (id == null)
            {
                return NotFound();
            }

            var messages = await _context.Messages
                .Include(m => m.Reciever)
                .Include(m => m.Sender)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (messages == null)
            {
                return NotFound();
            }

            return View(messages);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ViewBag.NavigatedTO = "Message";
            var messages = await _context.Messages.FindAsync(id);
            _context.Messages.Remove(messages);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MessagesExists(int id)
        {
            ViewBag.NavigatedTO = "Message";
            return _context.Messages.Any(e => e.Id == id);
        }
    }
}
