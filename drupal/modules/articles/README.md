# Articles filter for view

- This module provides a filter for view which filter out the latest Article published
- It shows option for Latest promoted article

## Configure

1. Create yml file
2. Create module file which will implement hook_views_data_alter to register the new filter
3. Create the filter class which extends InOperator class. The following methods are needed to be implemented
   - \_\_contruct() : Dependency injection
   - create() : to be autoloaded
   - init
   - getArticles : for call back
4. The hooks in views.inc file are called when the filter is used
5. The hooks in articles.module file are called each time even if filter is not used

## Install

- Enable the module
- Add the filter to a view and configure its settings
