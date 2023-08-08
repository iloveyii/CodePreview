<?php

namespace Drupal\actioner\Util;



class View
{
  public static function getFields($view)
  {
    $rows = [];

    foreach ($view->result as $id => $row) {
      $data = [];
      $r = new \ReflectionObject($row->_entity);
      $fields = $r->getProperty('fields');
      $fields->setAccessible(true);
      $_data = ($fields->getValue($row->_entity));

      foreach ($_data as $k => $a) {
        $data[$k] = (reset($a))->first()->value;
      }

      $rows[] = $data;
    }

    return $rows;
  }
}
