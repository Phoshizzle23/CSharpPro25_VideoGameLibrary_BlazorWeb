using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleVideoGameLibrary.Server.Data;
using SimpleVideoGameLibrary.Shared;

namespace SimpleVideoGameLibrary.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGameController : ControllerBase
    {
        private readonly DataContext _context;

        public VideoGameController(DataContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<List<VideoGame>>> GetAllVideoGames()
        {
            var list = await _context.VideoGames.OrderBy(g => g.ReleaseYear).ToListAsync();

            return Ok(list);
                
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VideoGame>> GetAllVideoGame(int id)
        {
            var dbVideoGame = await _context.VideoGames.FindAsync(id);
            if (dbVideoGame == null)
            {
                return NotFound("This game does not exist!");
            }

            return Ok(dbVideoGame);

        }

        [HttpPost]
        public async Task<ActionResult<List<VideoGame>>> CreateVideoGames(VideoGame videoGame)
        {
            _context.VideoGames.Add(videoGame);
            await _context.SaveChangesAsync();

            return await GetAllVideoGames();

        }

        [HttpPut("{id}")]        
        public async Task<ActionResult<List<VideoGame>>> UpdateVideoGames(int id, VideoGame videoGame)
        {
            var dbVideoGame = await _context.VideoGames.FindAsync(id);
            if(dbVideoGame == null)
            {
                return NotFound("This game does not exist!");
            }
            dbVideoGame.Title = videoGame.Title;
            dbVideoGame.ReleaseYear = videoGame.ReleaseYear;
            dbVideoGame.Publisher = videoGame.Publisher;

            _context.VideoGames.Add(videoGame);
            await _context.SaveChangesAsync();

            return await GetAllVideoGames();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<VideoGame>>> DeleteVideoGame(int id)
        {
            var dbVideoGame = await _context.VideoGames.FindAsync(id);
            if (dbVideoGame == null)
            {
                return NotFound("This game does not exist!");
            }
            _context.VideoGames.Remove(dbVideoGame);
            await _context.SaveChangesAsync();

            return await GetAllVideoGames();

        }
    }
}
