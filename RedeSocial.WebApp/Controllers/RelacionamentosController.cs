using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RedeSocial.Domain.Entities;
using RedeSocial.Infrastructure.Context;

namespace RedeSocial.WebApp.Controllers
{
    public class RelacionamentosController : Controller
    {
            private readonly RedeSocialDbContext _context;

            public RelacionamentosController(RedeSocialDbContext context)
            {
                _context = context;
            }

            // GET: Amizade
            public async Task<IActionResult> Index()
            {
                var redeSocialDbContext = _context.Relacionamentos.Include(a => a.PerfilA).Include(a => a.PerfilB);
                return View(await redeSocialDbContext.ToListAsync());
            }

            // GET: Amizade/Details/5
            public async Task<IActionResult> Details(Guid id)
            {
                if (id == null || _context.Relacionamentos == null)
                {
                    return NotFound();
                }

                var relacionar = await _context.Relacionamentos
                    .Include(a => a.PerfilA)
                    .Include(a => a.PerfilB)
                    .FirstOrDefaultAsync(m => m.PerfilIdA == id);
                if (relacionar == null)
                {
                    return NotFound();
                }

                return View(relacionar);
            }

            // GET: Amizade/Create
            public IActionResult Create()
            {
                ViewData["PerfilIdA"] = new SelectList(_context.Perfils_, "Id", "Nome");
                ViewData["PerfilIdB"] = new SelectList(_context.Perfils_, "Id", "Nome");
                return View();
            }

            // POST: Amizade/Create
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(Relacionamento relacionamento)
            {
                ViewData["PerfilIdA"] = new SelectList(_context.Perfils_, "Id", "Nome", relacionamento.PerfilIdA);
                ViewData["PerfilIdB"] = new SelectList(_context.Perfils_, "Id", "Nome", relacionamento.PerfilIdB);
                if (ModelState.IsValid)
                {
                    if (relacionamento.PerfilIdA == relacionamento.PerfilIdB)
                    {
                        ModelState.AddModelError(string.Empty, "Erro: amizade não pode ser da mesma pessoa");
                        return View(relacionamento);
                    }
                    try
                    {
                        _context.Add(relacionamento);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError(string.Empty, "Erro: amizade já existe");
                        return View(relacionamento);
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(relacionamento);
            }

            // GET: Amizade/Edit/5
            public async Task<IActionResult> Edit(Guid id)
            {
                if (id == null || _context.Relacionamentos == null)
                {
                    return NotFound();
                }

                var relacionar = await _context.Relacionamentos
                    .Include(a => a.PerfilA)
                    .Include(a => a.PerfilB)
                    .FirstOrDefaultAsync(m => m.PerfilIdA == id);
                if (relacionar == null)
                {
                    return NotFound();
                }
                ViewData["PerfilIdA"] = new SelectList(_context.Perfils_, "Id", "Nome", relacionar.PerfilIdA);
                ViewData["PerfilIdB"] = new SelectList(_context.Perfils_, "Id", "Nome", relacionar.PerfilIdB);
                return View(relacionar);
            }

            // POST: Amizade/Edit/5
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(Guid id, [Bind("PessoaIdA,PessoaIdB")] Relacionamento relacionamento)
            {
                //if (id != amizade.PessoaIdA)
                //{
                //    return NotFound();
                //}

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(relacionamento);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!AmizadeExists(relacionamento.PerfilIdA))
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
                ViewData["PerfilIdA"] = new SelectList(_context.Perfils_, "Id", "Nome", relacionamento.PerfilIdA);
                ViewData["PerfilIdB"] = new SelectList(_context.Perfils_, "Id", "Nome", relacionamento.PerfilIdB);
                return View(relacionamento);
            }

            // GET: Amizade/Delete/5
            public async Task<IActionResult> Delete(Guid id)
            {
                if (_context.Relacionamentos == null)
                {
                    return NotFound();
                }
                var relacionamento = await _context.Relacionamentos
                    .Include(a => a.PerfilA)
                    .Include(a => a.PerfilB)
                    .FirstOrDefaultAsync(m => m.PerfilIdA == id);
                if (relacionamento == null)
                {
                    return NotFound();
                }
                return View(relacionamento);
            }

            // POST: Amizade/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(Relacionamento relacionamento)
            {

                if (_context.Relacionamentos == null)
                {
                    return Problem("Entity set 'AT_AzureContext.Amizade'  is null.");
                }
                if (relacionamento != null)
                {
                    _context.Relacionamentos.Remove(relacionamento);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            private bool AmizadeExists(Guid id)
            {
                return (_context.Relacionamentos?.Any(e => e.PerfilIdA == id)).GetValueOrDefault();
            }
        }
    }

