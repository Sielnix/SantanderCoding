## Building and running the application

To build, test and run the app inside docker run following commands:
```shell
docker build -t santander-coding .
docker run -p 8080:8080 -p 8081:8081 santander-coding
```

Swagger of website is then available under following url:
http://localhost:8080/swagger/index.html

Best stories endpoint is available under:
http://localhost:8080/best-stories/items?limit=10

`limit` parameter is optional, it's allowed values range is 1-200, default 10.

## Assumptions

Code assumes, that there is no rate limiting on HackerNews api. It is stated [here](https://github.com/HackerNews/API#uri-and-versioning).

## Enhancements to make

#### Cache
There is a great area for cache improvement. Stories, fetched from HackerNews, could be cached, so they wouldn't be repeatedly fetched from external service. IDs of best stories could also be cached. Cache over generated result (json) could also be introduced.

#### End to end tests

Currently, there's only one unit test. E2E tests introduction would be a great improvement.

#### Structured logging

Logging with easy json/yaml configuration for development, testing and production usages is a must have for production deployments. Easiest way to do it is use Serilog combined with Microsoft.Logging.

#### Authentication

Authentication to app is a must-have in most modern web apps, altough it's not required in this demo. Most common implementations are using JWT tokens.