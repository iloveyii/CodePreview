# Repairing services

## Tips

- If a module is not shown in Extend list, make sure that the module folder is not empty. Sometimes the directory exists but the files are not copied due to git sub-modules.

```
   public function showCustomForm() {
        $node = \Drupal::entityTypeManager()->getStorage('node')->load('123');
        $edit_form = \Drupal::entityTypeManager()->getFormObject('node', 'my_custom_form_display_mode_id')->setEntity($node);
        return \Drupal::formBuilder()->getForm($edit_form);
    }

    function mymodule_entity_type_build(array &$entity_types) {
      $entity_types['node']->setFormClass('my_custom_form_display_mode_id', 'Drupal\node\NodeForm');
    }
```

- Render view : \Drupal::service('renderer')->render(views_embed_view('view_id', 'display_id',\$arg));
- If you want to add your custom validator in addition to the default, you will have to add something like this in hook_form_FORM_ID_alter (or similar):
- `$form['#validate'][] = 'my_test_validate'`;
- Increase taxonomy page size `docker exec -ti jojomobil_drupal /opt/vendor/drush/drush/drush config:set taxonomy.settings terms_per_page_admin 500`

## Links

- https://gorannikolovski.com/blog/how-loop-through-referenced-entities
- https://programmingmentor.pk/blog/drupal-10-get-paragraph-field-value-programmatically
- https://drupal.stackexchange.com/questions/222260/add-content-type-field-programmatically
- https://programmingmentor.pk/blog/drupal-10-create-child-term-parent-programmatically
- https://www.axelerant.com/blog/drupal-8-avoid-common-mistakes-for-better-code
- https://davidjguru.github.io/blog/drupal-tips-from-array-to-render-html
- https://drupal.stackexchange.com/questions/255419/programmatically-render-a-node-edit-form-with-a-custom-form-display-mode
- SFTP: https://gist.github.com/devudit/04d0430b48462ad201de8ce05a208085
- https://www.drupal.org/docs/user_guide/en/structure-reference-fields.html
- https://gist.github.com/davidjguru/3b9d36bf3e00dd338d751b7bfa2c41eb
- Drupal consultancy: https://www.specbee.com/drupal-web-development-services
- CI CD with drupal : https://www.specbee.com/blogs/continuous-integration-and-testing-in-drupal
- Design: https://www.dstro.co/

## Setup Google API for reading Googe Map Reviews

- Dashboard: https://console.cloud.google.com/apis/credentials?organizationId=0&project=jojomobil-380412
- API key: AIzaSyBxQCAzWXX7qpjXk6L466mL7LgvpMQFSAg
- Place ID: ChIJn1JJsFIDVEYRYzlv_neSSJs
- URL: https://maps.googleapis.com/maps/api/place/details/json?placeid=ChIJn1JJsFIDVEYRYzlv_neSSJs&key=AIzaSyBxQCAzWXX7qpjXk6L466mL7LgvpMQFSAg
- https://developers.google.com/maps/documentation/embed/get-api-key
- https://developers.google.com/maps/documentation/javascript/examples/places-placeid-finder
- https://developers.google.com/my-business/content/review-data
- https://console.cloud.google.com/apis/credentials?project=jojomobil-380412
- GET
  - https://mybusiness.googleapis.com/v4/accounts/reviews
  - oAuth



## Setup Google API for sending emails via PHPMailer

- https://github.com/PHPMailer/PHPMailer/wiki/Using-Gmail-with-XOAUTH2
- All your Google projects : https://console.cloud.google.com/cloud-resource-manager
- Enable API from here: https://console.cloud.google.com/apis/library?project=jojomobil-380412
- Not fixed - use below instead

## Setup Drupal Symfony Mailer

- https://www.drupal.org/docs/contributed-modules/drupal-symfony-mailer
- https://support.loopia.se/wiki/e-postservrar/

