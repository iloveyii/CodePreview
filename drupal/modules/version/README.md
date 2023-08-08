# Version command

- It is used to bump a new version based on argument (major, minor, bug, build)
- It reads current version from .env & save the new calculated version at .env, build/install.txt & database table version

## Run command

- Major `docker exec -ti cache_drupal /opt/drush version:bump --major`
- Minor `docker exec -ti cache_drupal /opt/drush version:bump --minor`
- Bug `docker exec -ti cache_drupal /opt/drush version:bump --bug`
- Build `docker exec -ti cache_drupal /opt/drush version:bump --build`
- Dry run, does not save any files `docker exec -ti cache_drupal /opt/drush version:bump --build --dry`

SELECT \*, CONVERT(data USING utf8) FROM `config`
WHERE name in ('dbbackup.settings', 'version.settings');
