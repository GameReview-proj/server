﻿namespace GameReview.Services.Exceptions;

public class ConflictException(string message) : Exception(message)
{ }