using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUDwebapi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUDwebapi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly MovieDBContext moviedbcontext;
        public MovieController(MovieDBContext movieDbContext)
        {
            moviedbcontext = movieDbContext;
        }

        [HttpGet]
        public IEnumerable<MovieDK> GetMovies()
        {
            return moviedbcontext.movie.ToList();
        }
        [HttpGet("GetMovieById")]
        public MovieDK GetMovieById(int Id)
        {
            return moviedbcontext.movie.Find(Id);
        }
        [HttpPost("InsertMovie")]
        public IActionResult InsertMovie([FromBody] MovieDK movie)
        {
            if (movie.Id.ToString() != "")
            {

                moviedbcontext.movie.Add(movie);
                moviedbcontext.SaveChanges();
                return Ok("Movie details saved successfully");
            }
            else
                return BadRequest();
        }
        [HttpPut("UpdateTutorial")]
        public IActionResult UpdateTutorial([FromBody] MovieDK movie)
        {
            if (movie.Id.ToString() != "")
            {
                moviedbcontext.Entry(movie).State = EntityState.Modified;
                moviedbcontext.SaveChanges();
                return Ok("Movie details Updated successfully");
            }
            else
                return BadRequest();
        }
        [HttpDelete("DeleteTutorial")]
        public IActionResult DeleteTutorial(int Id)
        {
            var result = moviedbcontext.movie.Find(Id);
            moviedbcontext.movie.Remove(result);
            moviedbcontext.SaveChanges();
            return Ok("Movie details Deleted successfully");
        }
    }
}
