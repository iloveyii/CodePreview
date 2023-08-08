<?php

/**
 * @file
 * @Contains Drupal\menu\Controller\MenuController
 */

namespace Drupal\menu\Controller;

use Drupal\Core\Controller\ControllerBase;
use Drupal\simplify_menu\MenuItems;
use Symfony\Component\DependencyInjection\ContainerInterface;



class MenuController extends ControllerBase
{

    /**
     * MenuItems definition.
     *
     * @var \Drupal\simplify_menu\MenuItems
     */
    protected $menuItems;

    /**
     * MenuItemsTwigExtension constructor.
     *
     * @param \Drupal\simplify_menu\MenuItems $menuItems
     *   The MenuItems service.
     */
    public function __construct(MenuItems $menuItems)
    {
        $this->menuItems = $menuItems;
    }

    /**
     * {@inheritdoc}
     */
    public static function create(ContainerInterface $container)
    {
        return new static(
            new MenuItems($container->get('menu.link_tree'))
        );
    }

    /**
     * List menu items
     * 
     * url (menu/links)
     */
    public function index()
    {
        // Don't cache this page
        $content['#cache']['max-age'] = 0;
        $data = $this->menuItems->getMenuTree('main');

        return [
            '#theme' => 'main',
            '#items' => $data,
        ];
    }
}
