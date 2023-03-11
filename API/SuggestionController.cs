using COSuggestionBox.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace COSuggestionBox.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuggestionController : ControllerBase
    {
        private readonly SuggestionContext _suggestionContext = new SuggestionContext();
        [HttpGet]
        public string Get()
        {
            return "Good morning!";
        }

        [HttpPost("")]
        public async Task<bool> Post([FromBody] SuggestionPostModel postModel)
        {
            try
            {
                Suggestion suggestion = new Suggestion
                {
                    Status = 1,
                    DateCreated = DateTime.Now,
                    Comment = postModel.Comment,
                    UserId = postModel.UserId
                };
                await _suggestionContext.Suggestions.AddAsync(suggestion);
                await _suggestionContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpGet("GetSuggestions")]
        public List<SuggestionReturnModel> GetSuggestions()
        {
            try
            {
                //return _suggestionContext.Suggestions.ToList();
                var query = _suggestionContext.Suggestions
                    .Where(s => s.DateFinished == null)
                    .Join(
                        _suggestionContext.SuggestionStatuses,
                        suggestion => suggestion.Status,
                        status => status.Id,
                        (suggestion, status) => new SuggestionReturnModel
                        {
                            SuggestionId = suggestion.Id,
                            Comment = suggestion.Comment,
                            DateCreated = suggestion.DateCreated,
                            Status = status.Status,
                            UserId = suggestion.UserId
                        });

                return query.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost("UpdateStatus")]
        public async Task<string> UpdateStatus([FromBody] UpdateStatusPostModel update)
        {
            try
            {
                Suggestion suggestion = await _suggestionContext.Suggestions.FirstAsync(s => s.Id == update.SuggestionId);
                if (update.StatusId == 6)
                {
                    suggestion.DateFinished = DateTime.Now;
                } else
                {
                    suggestion.Status = update.StatusId;
                }
                suggestion.DateUpdated = DateTime.Now;
                await _suggestionContext.SaveChangesAsync();
                SuggestionStatus status = await _suggestionContext.SuggestionStatuses.FirstAsync(s => s.Id == update.StatusId);
                return status.Status;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
