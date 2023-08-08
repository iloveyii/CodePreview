<?php

namespace Drupal\dbbackup\Models;

use Drupal\dbbackup\Models\IBackup;
use Drupal\dbbackup\Models\Helper;
use Drupal\dbbackup\Models\File;
use Drupal\dbbackup\Models\BaseBackup;
use mikehaertl\shellcommand\Command;


class DockerBackup extends BaseBackup implements IBackup
{
    private $container_name, $host, $db_name, $db_user, $db_pass;

    /**
     * Constructor
     * 
     * Backup using docker exec command
     */
    public function __construct($container_name, $host, $db_name, $db_user, $db_pass)
    {
        $this->container_name = $container_name;
        $this->host = $host;
        $this->db_name = $db_name;
        $this->db_user = $db_user;
        $this->db_pass = $db_pass;
    }

    /**
     * Get class name in lowercase
     */
    public function getClassName()
    {
        $parts = explode('\\', __CLASS__);
        return strtolower(array_pop($parts));
    }

    /**
     * @inheritdoc
     */
    public function getBackup()
    {
        $command = new Command([
            'command' => 'docker exec $container /usr/bin/mysqldump -h$host --no-tablespaces -u$user -p$pass $db > $path',
            'procEnv' => [
                'container' => $this->container_name,
                'host' => $this->host,
                'user' => $this->db_user,
                'pass' => $this->db_pass,
                'db' => $this->db_name,
                'path' => $this->getBackupFilePath()
            ]
        ]);

        if ($command->execute()) {
            $command->getOutput();
            Helper::logger("Command Ran Successfully: " . $command->getCommand());
            return File::setFilePath($this->getBackupFilePath())->fileExists()->notOlderThan(3)->sizeIsGreater()->resultIsTrue();
        } else {
            Helper::logger("Command Ran Failed: " . $command->getCommand() . ' Err: ' . $command->getError() . ' exit code:' . $exitCode = $command->getExitCode());
            return false;
        }
    }

    /**
     * @inheritdoc
     */
    public function isEnabled()
    {
        return `which docker`;
    }
}
