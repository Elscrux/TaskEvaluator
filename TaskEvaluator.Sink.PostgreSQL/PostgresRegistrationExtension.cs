﻿using Microsoft.Extensions.DependencyInjection;
using TaskEvaluator.Modules;
using TaskEvaluator.Sinks;
namespace TaskEvaluator.Sink.PostgreSQL;

public static class PostgresRegistrationExtension {
    public static TaskEvaluatorConfiguration AddPostgreSQL(this SinkConfig sink) {
        sink.Services.Configure<PostgresSinkConfiguration>(sink.Configuration.GetSection("Database"));
        sink.Services.AddTransient<PostgresFinalResultSink>();
        sink.Services.AddTransient<IFinalResultSink, PostgresFinalResultSink>();

        return sink.Config;
    }
}
