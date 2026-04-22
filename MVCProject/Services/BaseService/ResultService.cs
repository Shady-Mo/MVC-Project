using Microsoft.AspNetCore.Identity;

namespace MVCProject.Services.BaseService {
    public class ResultService {
        public bool Succeeded { get; set; }
        public string? ErrorMessage { get; set; }
        public string? TargetProperty { get; set; }
        public bool? IsLockedOut { get; set; }
        public List<string>? Errors { get; set; } = [];

        public static ResultService Success() {
            return new ResultService { Succeeded = true };
        }

        public static ResultService Failure(string error, bool isLockedOut, string targetProperty = "") {
            return new ResultService { Succeeded = false, ErrorMessage = error,
                    IsLockedOut = isLockedOut, TargetProperty = targetProperty };
        }

        public static ResultService Failure(IEnumerable<IdentityError> identityErrors) {
            return new ResultService { Succeeded = false, Errors = identityErrors.Select(e => e.Description).ToList() };
        }
    }
}
