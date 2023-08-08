<?php

namespace Drupal\dbbackup\Controller;

use Drupal\dbbackup\Models\IBackup;
use Drupal\dbbackup\Models\Helper;
use Drupal\dbbackup\Models\File;
use Drupal\dbbackup\Models\BaseBackup;
use Drupal\Core\Controller\ControllerBase;
use Drupal\dbbackup\Models\DockerBackup;
use Drupal\dbbackup\Models\LocalBackup;
use Drupal\dbbackup\Models\SocatBackup;
use Symfony\Component\HttpFoundation\BinaryFileResponse;


class DownloadController extends ControllerBase
{
    /**
     * Downoload socat files
     */
    public function socat()
    {
        $socatBackup = new SocatBackup(null, null);
        $file_path = $socatBackup->getBackupFilePathZipped(); // '/opt/shell/log//backup.sql.gz';
        return $this->sendFile($file_path);
    }

    /**
     * Downoload dump files
     */
    public function dump()
    {
        $dumpBackup = new LocalBackup(null, null, null, null);
        $file_path = $dumpBackup->getBackupFilePathZipped();
        return $this->sendFile($file_path);
    }
    /**
     * Downoload dump files
     */
    public function docker()
    {
        $dumpBackup = new DockerBackup(null, null, null, null, null);
        $file_path = $dumpBackup->getBackupFilePathZipped();
        return $this->sendFile($file_path);
    }


    protected function sendFile($file_path)
    {
        if (Helper::fileExists($file_path)) {
            $headers = [
                'Content-Type'          => 'application/zip',
                'Content-Disposition'   => 'attachment; filename=' . basename($file_path),
                'Content-length'        => filesize($file_path),
                'Pragma'                => 'no-cache',
                'Expires'               => '0',
            ];
            return new BinaryFileResponse($file_path, 200, $headers, true);
        } else {
            $this->messenger()->addStatus($this->t("File at location @file does not exist", ['@file' => $file_path]));
            return [
                '#markup' => $this->t("File at location @file does not exist", ['@file' => $file_path])
            ];
        }
    }
}
