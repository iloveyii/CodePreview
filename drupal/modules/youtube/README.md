# Youtube Plugin

- A Drupal field with formatter and widget plugins to show embedded video of youtube from URL.

## Create Field Plugin

1. Create info yml file
2. Create Field directories `mkdir -p src/Plugin/Field` && `cd src/Plugin/Field` && `mkdir Field{Formatter,Type,Widget}`
3. Add Class files `YoutubeFieldType, YoutubeFieldFormatter, YoutubeFieldWidget` in corresponding directories.
4. Add template file, css file, libraries yml file, and module file.

## Tips

- When using in views, inside style settings choose 'Use field template'
