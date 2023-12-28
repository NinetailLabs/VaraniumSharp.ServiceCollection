/*
 * Upload coverage results to OpenCover
 */

#region Tasks

// Uploads Code Coverage results
Task ("CoverageUpload")
	.Does (() => {
        var blockText = "CoverageUpload";
        StartBlock(blockText);

        if(runningOnLocal)
        {
            Information("Coverage results are not uploaded for local builds");
            return;
        }

		var coverallRepoToken = EnvironmentVariable("CoverallRepoToken");
        if(string.IsNullOrEmpty(coverallRepoToken))
        {
            Warning("Could not find Coverall token - Coverage results will not be uploaded");
            return;
        }

		var args = $"coverlet --opencover -i {coverPath} --repoToken {coverallRepoToken}";

        EndBlock(blockText);
	});

#endregion