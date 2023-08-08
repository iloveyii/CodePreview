<?php

/**
 * @file
 * @Contains Drupal\animate\Controller\DemoController
 */

namespace Drupal\animate\Controller;

use Drupal\Core\Controller\ControllerBase;
use Drupal\simplify_menu\MenuItems;
use Symfony\Component\DependencyInjection\ContainerInterface;



class DemoController extends ControllerBase
{

    /**
     * Shows list of animations
     */
    public function index()
    {
        return [
            '#theme' => 'index',
            '#items' => [
                'be' => $this->bounceExpand(),
                'lu' => $this->lineup(),
                'rv' => $this->revealing(),
                '3d' => $this->threed(),
                'smoothe' => $this->smoothe(),
            ]
        ];
    }

    public function  bounceExpand()
    {
        return [
            '#type' => 'bounce_expand',
        ];
    }

    public function  lineup()
    {
        return [
            '#type' => 'lineup',
        ];
    }

    public function  revealing()
    {
        return [
            '#type' => 'revealing',
            '#showup' => 'ShowUp',
            '#slidein' => 'Text to slide in'
        ];
    }

    public function  threed()
    {
        return [
            '#type' => 'threed',
            '#text' => 'Soft\a    Hem!',
            '#size' => 'md'
        ];
    }

    public function  smoothe()
    {
        return [
            '#type' => 'smoothe',
            '#text' => 'Soft\a    Hem!',
            '#size' => 'md'
        ];
    }
}
