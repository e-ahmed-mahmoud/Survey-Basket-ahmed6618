namespace SurveyBasket.Controllers;


[Route("api/[controller]")]
[ApiController]
public class PollsController(IPollService pollService) : ControllerBase
{
    private readonly IPollService _pollService = pollService;

    [HttpGet("[action]")]
    public IActionResult GetPolls() => Ok(_pollService.GetAllPolls());


    [HttpGet( "[action]/{id}")]
    public IActionResult GetById(int id)
    {
        var poll = _pollService.GetById(id);
        return poll is null ? NotFound() : Ok(poll);
    }

    [HttpPost("[action]")]
    public IActionResult AddPoll([FromBody] Poll poll)
    {
        var newPoll = _pollService.Add(poll);

        return CreatedAtAction(nameof(GetById), routeValues: new { id = newPoll.Id }, newPoll);
    }

    [HttpPut("[action]/{id}")]
    public IActionResult UpdatePoll(int id,[FromBody] Poll poll)
    {

        var isUpdated = _pollService.Update(id,poll);


        return !isUpdated ? NotFound() : NoContent();
    }


    [HttpDelete("[action]/{id}")]
    public IActionResult DeletePoll(int id) => _pollService.Delete(id) ? NoContent() : NotFound();

}
