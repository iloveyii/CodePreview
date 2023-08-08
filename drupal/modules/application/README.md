# Application

- Apply on challenges
- This module uses most of the interesting features of Drupal module development

## Configuration

- Add info file
- module file

## Download Command

- `field_hi_experimental_data` must never be changed/move to another location as it is dealt privately
- Shows all FILES current location and the new location from the json file except field_hi_experimental_data
  - `docker exec -ti cache_drupal /opt/drush download:json.show --application_form`
- Run commands - for testing
  - `docker exec -ti cache_drupal /opt/drush download:run --delete`
- https://gorannikolovski.com/blog/drupal-create-file-programmatically#:~:text=Finally%2C%20we%20can%20now%20create,file%20entity%20is%20not%20enough.
