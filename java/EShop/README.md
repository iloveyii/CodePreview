# T-8 Project

## eShop Project

For this class you are to develop a website (at the code level) for online shopping with the following main features:
Catalog of items with inventory management and search capability:

* Customer registration and login security
* Adding and removing items for a shopping cart
* Order with payment gateway option
* Packaging and shipping as defined by customer
* Website with friendly responsive UI and UX

Only two actors, the customer and the owner who interact with the eShop. You would think of all actions they may do when they
interact with system and based on the previous 6 subfunctions:
![Use Case Diagram](./fig/use-case-diagram.png)

## Coding Guidelines

Commit message should follow the [Conventional Commits](https://www.conventionalcommits.org/en/v1.0.0/) specification. This make
contributions easily comprehensible and enables us to automatically generate release notes.

Recommended tooling for developers:

* JetBrains Plugin [Conventional Commit](https://plugins.jetbrains.com/plugin/13389-conventional-commit)
  by [Edoardo Luppi](https://github.com/lppedd)
* Visual Studio
  Plugin [Conventional Commits](https://marketplace.visualstudio.com/items?itemName=vivaxy.vscode-conventional-commits)
  by [vivaxy](https://marketplace.visualstudio.com/publishers/vivaxy)

**Example commit message**

```
fix: prevent racing of requests

Introduce a request id and a reference to latest request. Dismiss
incoming responses other than from latest request.

Remove timeouts which were used to mitigate the racing issue but are
obsolete now.

Reviewed-by: Z
Refs: #123
```

### Git Workflow

We will follow a short paced [Gitflow](https://www.atlassian.com/git/tutorials/comparing-workflows/gitflow-workflow) variation.
Since Gitflow is considered a legacy Git workflow we will only use the branch structure (see image below).
![Git Branches](https://wac-cdn.atlassian.com/dam/jcr:34c86360-8dea-4be4-92f7-6597d4d5bfae/02%20Feature%20branches.svg?cdnVersion=296)
Branches `main` and `develop` are projected by branch policies and do not allow commits to be push into them. When you start
work on a ticket pull the latest version of the `development` branch and create a new feature-branch from `development`. This
branch should follow this naming convention:

| Branch Name                                     | Description                                                                                                                                                                                                                                                                                                        |
| ----------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| `main`                                        | This branch is the default branch available in the Git repository. Team members need to keep the master branch stable and updated. It usually is stable and doesn't allow direct check-in. Merging is possible only after code review as part of a merge request. This branch should only contain tagged versions. |
| `development`                                 | This is the main development branch. This is the branch all merge request will target. Before merging to the master, changes made in the dev branch undergo reviews and tests.                                                                                                                                     |
| `feature/{ticket-number}/{short-description}` | These branches are for active development and contain uncompleted work. Once the system is back to a stable state after making changes open a new merge request (MR) into `development`.                                                                                                                         |
| `bugfix/{ticket-number}/{short-description}`  | These branches are for fixing bugs. Similar to feature-branches a MR needs to be opened once work is completed on these branches.                                                                                                                                                                                  |

You can create and delete feature and bugfix branches whenever it is required. When you want to make a contribution (merge you
work) [open a new merge request](https://git.chalmers.se/courses/eda397/2122/team-8/t-8-project/-/merge_requests) (MR) so that
you code can be reviewed. Merging often is good practice and is the best way to get your code into the master branch without
merge conflicts 🙂🚀.

---

## Badges

On some READMEs, you may see small images that convey metadata, such as whether or not all the tests are passing for the
project. You can use Shields to add some to your README. Many services also have instructions for adding a badge.

## Visuals

Depending on what you are making, it can be a good idea to include screenshots or even a video (you'll frequently see GIFs
rather than actual videos). Tools like ttygif can help, but check out Asciinema for a more sophisticated method.

## Technical Information

This is a fullstack web application built with the following stack:

* [Spring](https://spring.io/) for the backend server.
* [Angular](https://angular.io/) with [Angular Material](https://material.angular.io/) for the frontend website.
* [MySQL](https://www.mysql.com/) for the database.
* [Docker](https://www.docker.com/) and [Docker Compose](https://docs.docker.com/compose/) to containarize the above.

## Installation

To run the backend, make sure to download and setup the latest [Java JDK](https://www.oracle.com/java/technologies/downloads/)
and [Apache Maven](https://maven.apache.org/download.cgi) then navigate to the `backend` directory and run the following two
commands:

1. `mvn clean install`
2. `mvn spring-boot:run`

---

To run the frontend, make sure to download and setup the latest [Node.js](https://nodejs.org/en/) then navigate to
the `frontend` directory and run the following three commands:

1. `npm install -g @angular/cli`
2. `npm install`
3. `ng serve`

---

To build and run the whole application:

1. Make sure to have a [.env](https://docs.docker.com/compose/env-file/) file in the root folder with the following arguments:
   * `DATABASE_USER`
   * `DATABASE_ROOT_PASSWORD`
   * `DATABASE_NAME`
   * `DATABASE_LOCAL_PORT` (Recommended default: 3306)
   * `DATABASE_DOCKER_PORT` (Recommended default: 3306)
   * `BACKEND_LOCAL_PORT` (Recommended default: 8080)
   * `BACKEND_DOCKER_PORT` (Recommended default: 8080)
   * `FRONTEND_LOCAL_PORT` (Recommended default: 4200)
   * `FRONTEND_DOCKER_PORT` (Recommended default: 4200)
2. Run the following `docker-compose` command:
   - `docker-compose --env-file ./.env up --build`
3. The backend will be available on [http://localhost:8080/]() and the frontend on [http://localhost:4200/]().
4. For developing purposes we added [Adminer](https://hub.docker.com/_/adminer/) a database management tool. You can access it
   on [http://localhost:8081/?server=database]() and use the credential specified in the `.env` file to log in.

## Usage

For convenience, we added a `Makefile`. You can run the following commands (if your system is Unix-based):

| Command        | Description                                    |
| -------------- | ---------------------------------------------- |
| `make build` | Builds all services.                           |
| `make start` | Runs all services.                             |
| `make stop`  | Stops all services.                            |
| `make down`  | Stops all services and removes all containers. |

If you need make on a Windows machine follow this [link](https://stackoverflow.com/a/2532239/11016410).

## Support

Tell people where they can go to for help. It can be any combination of an issue tracker, a chat room, an email address, etc.

## Roadmap

If you have ideas for releases in the future, it is a good idea to list them in the README.

## Contributing

State if you are open to contributions and what your requirements are for accepting them.

For people who want to make changes to your project, it's helpful to have some documentation on how to get started. Perhaps
there is a script that they should run or some environment variables that they need to set. Make these steps explicit. These
instructions could also be useful to your future self.

You can also document commands to lint the code or run tests. These steps help to ensure high code quality and reduce the
likelihood that the changes inadvertently break something. Having instructions for running tests is especially helpful if it
requires external setup, such as starting a Selenium server for testing in a browser.

## Authors and acknowledgment

Show your appreciation to those who have contributed to the project.

## License

For open source projects, say how it is licensed.

## Project status

If you have run out of energy or time for your project, put a note at the top of the README saying that development has slowed
down or stopped completely. Someone may choose to fork your project or volunteer to step in as a maintainer or owner, allowing
your project to keep going. You can also make an explicit request for maintainers.

## Tools Integration

- [Slack notifications](https://git.chalmers.se/courses/eda397/2122/team-8/t-8-project/-/integrations/slack/edit): To send
  notifications about project events to Slack.

## Collaborate with your team

- [ ] [Invite team members and collaborators](https://docs.gitlab.com/ee/user/project/members/)
- [ ] [Create a new merge request](https://docs.gitlab.com/ee/user/project/merge_requests/creating_merge_requests.html)
- [ ] [Automatically close issues from merge requests](https://docs.gitlab.com/ee/user/project/issues/managing_issues.html#closing-issues-automatically)
- [ ] [Enable merge request approvals](https://docs.gitlab.com/ee/user/project/merge_requests/approvals/)
- [ ] [Automatically merge when pipeline succeeds](https://docs.gitlab.com/ee/user/project/merge_requests/merge_when_pipeline_succeeds.html)

## Test and Deploy

Use the built-in continuous integration in GitLab.

- [ ] [Get started with GitLab CI/CD](https://docs.gitlab.com/ee/ci/quick_start/index.html)
- [ ] [Analyze your code for known vulnerabilities with Static Application Security Testing(SAST)](https://docs.gitlab.com/ee/user/application_security/sast/)
- [ ] [Deploy to Kubernetes, Amazon EC2, or Amazon ECS using Auto Deploy](https://docs.gitlab.com/ee/topics/autodevops/requirements.html)
- [ ] [Use pull-based deployments for improved Kubernetes management](https://docs.gitlab.com/ee/user/clusters/agent/)
- [ ] [Set up protected environments](https://docs.gitlab.com/ee/ci/environments/protected_environments.html)

---

# Editing this README

When you're ready to make this README your own, just edit this file and use the handy template below (or feel free to structure
it however you want - this is just a starting point!). Thank you to [makeareadme.com](https://www.makeareadme.com/) for this
template.

## Suggestions for a good README

Every project is different, so consider which of these sections apply to yours. The sections used in the template are
suggestions for most open source projects. Also keep in mind that while a README can be too long and detailed, too long is
better than too short. If you think your README is too long, consider utilizing another form of documentation rather than
cutting out information.
