<?php

namespace Drupal\dbbackup\Models;

use Drupal\dbbackup\Models\IRestore;

class Restore
{
    /**
     * @var Drupal\dbbackup\Models\IRestore
     */
    private $strategy;

    /**
     * Constructs a Restore object.
     *
     * @param Drupal\dbbackup\Models\IRestore $strategy
     *   The base strategy which should be used to create the restore file
     */
    public function __construct(IRestore $strategy)
    {
        $this->strategy = $strategy;
    }

    /**
     * Sets the restore strategy.
     *
     * @param Drupal\dbbackup\Models\IRestore $strategy
     *   The base strategy which should be used to create the restore file
     */
    public function setStrategy(IRestore $strategy)
    {
        $this->strategy = $strategy;
    }

    /**
     * Take restore based on strategy
     */
    public function startRestore()
    {
        return $this->strategy->startRestore();
    }

    /**
     * Check if strategy is enabled
     */
    public function isEnabled()
    {
        return $this->strategy->isEnabled();
    }

    /**
     * Get strategy
     */
    public function getStrategy()
    {
        return $this->strategy;
    }
}
