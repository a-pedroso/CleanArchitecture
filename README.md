## This solution is based on:

Jason Taylor and Mukesh Murugan
<br/>
https://github.com/jasontaylordev/CleanArchitecture
<br/>
https://github.com/iammukeshm/CleanArchitecture.WebApi

## Technologies
* .NET 5.0
* ASP.NET Core 5.0
* Entity Framework Core 5.0
* Swagger
* Serilog
* CQRS with MediatR
* Fluent Validation
* Health Checks
* App.Metrics
* OpenTelemetry

## License

This project is licensed with the [MIT license](LICENSE).

## Future work
- [ ] Add JWT Auth and a simple IDP
  - [x] client credentials
  - [ ] user authorization flow
- [x] Containerize WebApi
  - [x] add docker compose
  - [x] add redis for data protection
  - [x] add loki and/or seq for logs
- [ ] Separation of Reads and Writes
  - [ ] repositories
  - [ ] ORM for writes, dapper for reads
- [ ] refactor application 
  - [ ] towards task based and not CRUD ops
  - [x] move DTOs to queries folder and remove them from common
  - [x] wrappers: rename response to result w/ empty, data and paged data result
