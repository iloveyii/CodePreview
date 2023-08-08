<?php

namespace Drupal\dbbackup\Models;

interface IRestore
{
    public function isEnabled();

    public function startRestore();
}
