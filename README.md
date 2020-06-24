# ReshapeBackend

## Microservice-based ~~image-processing~~ user crud software

### How to run
Run development mode with `docker-compose up`  
Some day in the far future when this is production ready, run production mode with  
`docker-compose -f docker-compose.yml -f docker-compose.prod.yml up`

### How to use
Once the docker-compose up command has been run for both the backend and frontend projects (found [here](https://github.com/tlien/ReshapeFrontend/))
the following features will be available:

> \<container name> | \<url>

**The super basic frontend of the application, use this for testing the login and authorization.**
> `reshapefrontend_spa_1` | `localhost:5300`

**This is the identity server url, it provides login/logout and a simple diagnostics view of the logged-in user.**
> `reshapebackend_identity.svc_1` | `localhost:5200`

**This is the business management API, it will redirect to a swagger ui where you can test the login and the various endpoints of this API.**
> `<reshapebackend_bm.api_1>` | `localhost:5001`

**Same as above except it's the account API.**
> `reshapebackend_acc.api_1` | `localhost:5002`

All databases use postgres and are exposed on the following urls:
**Identity Server persisted grants and device codes DB.**
> `reshapebackend_identity.db_1` | `localhost:5433`

**Business management API DB.**
> `reshapebackend_bm.db_1` | `localhost:5434`

**Account API DB.**
> `reshapebackend_acc.db_1` | `localhost:5435`

Either use the [pgadmin](https://www.pgadmin.org/download/) management ui tool to inspect (user/pwd: postgres/example).  
or use the console tool with the command `docker exec -it <container name> psql -U postgres`.

The RabbitMQ instance can be found here:  
**This exposes a ui for managing and monitoring the RabbitMQ instance (user/pwd: guest/guest).**
> `reshapebackend_rabbitmq_1` | `localhost:5100`
