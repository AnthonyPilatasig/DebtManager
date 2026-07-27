using System;

namespace DebtManager.Frontend.Api;

public record AuthRequest(string Email);

public record AuthResponse(Guid UserId);
