using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using restAPI.Contracts;
using restAPI.Contracts.V1;
using restAPI.Contracts.V1.Reqests;
using restAPI.Contracts.V1.Responses;
using restAPI.Domain;
using restAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

namespace restAPI.Controllers.V1
{
    // Adds Authentication to all Endpoints in this controller 
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PostsController : Controller
    {

        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }


        /*
         * GET
         * api/v1/posts
         * Gets all obj
         */
        [HttpGet(ApiRoutes.Posts.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _postService.GetPosts());
        }

        /*
         * GET
         * api/v1/posts/{postId}
         * Param: postId (Requierd)
         * Return one
         */
        [HttpGet(ApiRoutes.Posts.Get)]
        public async Task<IActionResult> Get([FromRoute] Guid postId)
        {
            var post = await _postService.GetPostById(postId);

            if (post == null)
                return NotFound();

            return Ok(post);
        }

        /*
         * POST
         * api/v1/posts
         * Param: postRequest (Requierd)
         * Create new
         */
        [HttpPost(ApiRoutes.Posts.Create)]
        public async Task<IActionResult> Create([FromBody] CreatePostRequests postRequests)
        {
            var post = new Post { Name = postRequests.Name };

            await _postService.CreateNewPost(post);

            var baseURl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";

            // Location = Path to Get the create object
            var location = baseURl + "/" + ApiRoutes.Posts.Get.Replace("{postId}", post.Id.ToString());

            var response = new PostResponse { Id = post.Id };

            return Created(location, response);
        }

        /*
         * PUT
         * api/v1/posts/{postId}
         * Param: postId (Requierd), Request (Requierd)
         * Update full entity
         */
        [HttpPut(ApiRoutes.Posts.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid postId, [FromBody] UpdatePostRequest request)
        {
            var post = new Post
            {
                Id = postId,
                Name = request.Name
            };

            var updated = await _postService.UpdatePost(post);

            if (updated)
                return Ok(post);

            return NotFound();

        }

        /*
         * DELETE
         * api/v1/posts/{postId}
         * Param: postId (Requierd)
         * Delete given entity
         */
        [HttpDelete(ApiRoutes.Posts.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid postId)
        {
            var deleted = await _postService.DeletePost(postId);

            if (deleted)
                return NoContent();

            return NotFound();
        }
    }
}
