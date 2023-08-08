<?php

namespace Drupal\dbbackup\Models;

use Drupal\dbbackup\Models\IBackup;

class Backup
{
    /**
     * @var IBackup
     */
    private $strategy;

    /**
     * Constructs a Backup object.
     *
     * @param IBackup $strategy
     *   The base strategy which should be used to create the backup file
     */
    public function __construct(IBackup $strategy)
    {
        $this->strategy = $strategy;
    }

    /**
     * Sets the backup strategy.
     *
     * @param IBackup $strategy
     *   The base strategy which should be used to create the backup file
     */
    public function setStrategy(IBackup $strategy)
    {
        $this->strategy = $strategy;
    }

    /**
     * Take backup based on strategy
     */
    public function takeBackup()
    {
        return $this->strategy->getBackup();
    }

    /**
     * Check if strategy is enabled
     */
    public function isEnabled()
    {
        return $this->strategy->isEnabled();
    }

    /**
     * Take backup file path
     */
    public function getBackupFilePath()
    {
        return $this->strategy->getBackupFilePath();
    }

    /**
     * Get zipped file
     */
    public function getBackupFilePathZipped()
    {
        return $this->strategy->getBackupFilePathZipped();
    }

    /**
     * Get strategy
     */
    public function getStrategy()
    {
        return $this->strategy;
    }
}
