# Steeltoe Main Site

It is built using [Blazor](https://docs.microsoft.com/en-us/aspnet/core/blazor/) in Visual Studio. 

## Install Dependencies

```bash
dotnet new -i Microsoft.AspNetCore.Blazor.Templates::3.0.0-preview9.19465.2
```

## Local testing of Steeltoe site

This will run the site locally.

```bash
dotnet run
```

Visit site locally at [http://localhost](http://localhost)

<!-- ### Instructions for Updating Documentation Staging Site

All documentation will be updated in the `dev` branch. Once the documentation looks correct in the [Steeltoe Staging](https://steeltoe-staging.cfapps.io/) site, the dev branch should be merged into `master`. This will be the production site at [steeltoe.io](https://steeltoe.io) 

### Branches and Builds

#### Branch Build (1.x)
The `1.x` branch will build with `dev` and `master` branches.  There is no staging for the 1.x branch, so all commits will kick off a production build or staging build. The 1.x documentation will be available at [Steeltoe Staging 1.x](https://steeltoe-staging.cfapps.io/1x) and [Steeltoe.io 1.x](https://steeltoe.io/1x)

#### Development Build (dev)
Any commit to the `dev` branch is automatically built and added to the [Steeltoe Staging](https://steeltoe-staging.cfapps.io/) site.

#### Production Build (master)
***Important:*** Pushing the to the remote `master` branch will update the main [Steeltoe.io](https://steeltoe.io/) site.

Any commit to the `master` branch is automatically built and added to the [Steeltoe.io](https://steeltoe.io/) site. -->


