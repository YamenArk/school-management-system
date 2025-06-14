using Microsoft.AspNetCore.Mvc;

namespace School.Common.Exceptions
{
    public static class SchoolHttpExceptionMapper
    {
        public static ActionResult Map(Exception ex)
        {

            var errorResponse = new { message = ex.Message };

            if (ex is UserNotFoundException || ex is CourseNotFoundException)
                return new NotFoundObjectResult(errorResponse);

            if (ex is UnauthorizedException)
                return new UnauthorizedObjectResult(errorResponse);

            if (ex is EmailExistException || ex is DuplicateCourseTitleException)
                return new ConflictObjectResult(errorResponse);

            if (ex is InvalidResetCodeException || ex is StudentAlreadyInCourseException ||
             ex is OnlyStudentsAllowedException || ex is OnlyTeachersAllowedException)
                return new BadRequestObjectResult(errorResponse);

            if (ex is SchoolException)
                return new ObjectResult(errorResponse) { StatusCode = 500 };

            return new ObjectResult(new { message = "Unexpected error" }) { StatusCode = 500 };
        }
    }

}
