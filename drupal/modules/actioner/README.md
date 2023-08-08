# Actioner

## Install

- Enable the module
- Create Actions in `/admin/config/system/actions`, which will appear in Node bulk operations field in content area & views field settings for `Node operations bulk form`

## Configurations

- Class: ChangeBookingStatus

  - The taxanomy vocabulary id is `booking_statuses`
  - The field name in Article content type is `booking_status`

- Class: SendEmail
  - The taxanomy vocabulary id is `email_statuses`
  - The field name in Article content type is `email_status`

## Concepts

- Four areas of interest where you will find some events, and subscribers
  - Core Actions - on nodes and views
    - https://www.alansaunders.co.uk/blog/custom-actions-drupal-89
    - Examples: SendMail, ChangeBookingStatus
    - Requires: Actions module for saving configurations
  - ECA - Event condition action & related to Rules
    - Example:
    - https://www.drupal.org/docs/contributed-modules/rules-essentials/for-developers/actions
    - https://www.drupal.org/docs/contributed-modules/views-bulk-operations-vbo/creating-a-new-action
    - Requires: Rules to manage ECA UI
  - Subscribe and Dispatch Events - in modules:
    - Here create an Event class, dispatch it in hooks and create a EventSubscriber class to subscibe to the Event
    - Example: UserLoginEvent, UserLoginSubscriber
  - Hooks as listener:
    - A hook listens to built in ( or custom Events) and can dispatch Event using event.dispatcher service.
    - This is related to the above Subscribe & Dispatch
    - Example: function actioner_user_login(\$account)

https://www.drupal.org/project/vbo_export
