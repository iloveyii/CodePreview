<?php

namespace Drupal\dbbackup\Models;

interface IBackup
{
    public function isEnabled();

    public function getBackup();

    public function getBackupFilePath();
}
