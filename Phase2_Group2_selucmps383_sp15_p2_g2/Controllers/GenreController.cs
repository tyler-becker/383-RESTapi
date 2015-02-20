﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Phase2_Group2_selucmps383_sp15_p2_g2.Models;
using Phase2_Group2_selucmps383_sp15_p2_g2.DbContext;

namespace Phase2_Group2_selucmps383_sp15_p2_g2.Controllers
{
    public class GenreController : ApiController
    {
        private GameStoreContext db = new GameStoreContext();

        // GET api/Genre
        public IQueryable<Genre> GetGenres()
        {
            return db.Genres;
        }

        // GET api/Genre/5
        [ResponseType(typeof(Genre))]
        public IHttpActionResult GetGenre(string id)
        {
            Genre genre = db.Genres.Find(id);
            if (genre == null)
            {
                return NotFound();
            }

            return Ok(genre);
        }

        // PUT api/Genre/5
        public IHttpActionResult PutGenre(string id, Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != genre.GenreName)
            {
                return BadRequest();
            }

            db.Entry(genre).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenreExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Genre
        [ResponseType(typeof(Genre))]
        public IHttpActionResult PostGenre(Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Genres.Add(genre);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (GenreExists(genre.GenreName))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = genre.GenreName }, genre);
        }

        // DELETE api/Genre/5
        [ResponseType(typeof(Genre))]
        public IHttpActionResult DeleteGenre(string id)
        {
            Genre genre = db.Genres.Find(id);
            if (genre == null)
            {
                return NotFound();
            }

            db.Genres.Remove(genre);
            db.SaveChanges();

            return Ok(genre);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GenreExists(string id)
        {
            return db.Genres.Count(e => e.GenreName == id) > 0;
        }
    }
}