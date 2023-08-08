<?php

namespace Drupal\dbbackup\Models;

use Drupal\dbbackup\Models\IBackup;
use Drupal\dbbackup\Models\Helper;


class File
{
    private $file_path;
    static $instance;
    public $result = true;

    public function __construct($file_path)
    {
        $this->file_path = $file_path;
    }

    public static function setFilePath($file_path)
    {
        if (is_null(self::$instance)) {
            self::$instance = new File($file_path);
        }
        self::$instance->result = true;
        return self::$instance;
    }

    public function fileExists()
    {
        if (Helper::fileExists($this->file_path)) {
            $this->result = $this->result && true;
            Helper::logger(__CLASS__ . " fileExists, yes: " . $this->file_path);
            return $this;
        }
        Helper::logger(__CLASS__ . " fileExists, no: " . $this->file_path);
        $this->result = $this->result && false;
        return $this;
    }


    public function notOlderThan($hours)
    {
        // if ($this->fileExists()->resultIsTrue() &&  (time() - filemtime($this->file_path)) < ($hours * 3600)) {
        //     $this->result = $this->result && true;
        //     Helper::logger(__CLASS__ . " notOlderThan, yes: " . $hours);
        //     return $this;
        // }

        // Helper::logger(__CLASS__ . " notOlderThan, no: " . $hours);

        $this->result = $this->result && true;
        return $this;
    }

    public function sizeIsGreater()
    {
        if (Helper::fileExists($this->file_path)) {
            $sql = fopen($this->file_path, 'r');

            if ($sql != false && filesize($this->file_path) > 0) {
                $this->result = $this->result && true;
                Helper::logger(__CLASS__ . " sizeIsGreater, yes: " . filesize($this->file_path));
                return $this;
            }
        }
        Helper::logger(__CLASS__ . " sizeIsGreater, no: " . filesize($this->file_path));
        $this->result = $this->result && false;
        return $this;
    }

    public function resultIsTrue()
    {
        return $this->result == true;
    }

    public function getFilePath()
    {
        return $this->file_path;
    }
}
