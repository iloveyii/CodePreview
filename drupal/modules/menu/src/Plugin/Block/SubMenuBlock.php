<?php

namespace Drupal\menu\Plugin\Block;

use Drupal\Core\Block\BlockBase;
use Symfony\Component\DependencyInjection\ContainerInterface;
use Drupal\simplify_menu\MenuItems;
use Drupal\Core\Plugin\ContainerFactoryPluginInterface;
use Drupal\Core\Session\AccountInterface;


/**
 * Provides a 'Submenu' Block.
 *
 * @Block(
 *   id = "bs5submenu_block",
 *   admin_label = @Translation("BS 5 Sub Menu Block"),
 *   category = @Translation("Custom"),
 * )
 */
class SubMenuBlock extends BlockBase implements ContainerFactoryPluginInterface
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

    /**
     * @var AccountInterface $account
     */
    protected $account;

    /**
     * @param array $configuration
     * @param string $plugin_id
     * @param mixed $plugin_definition
     * @param \Drupal\Core\Session\AccountInterface $account
     */
    public function __construct(array $configuration, $plugin_id, $plugin_definition, AccountInterface $account, MenuItems $menuItems)
    {
        parent::__construct($configuration, $plugin_id, $plugin_definition);
        $this->account = $account;
        $this->menuItems = $menuItems;
    }

    /**
     * @param \Symfony\Component\DependencyInjection\ContainerInterface $container
     * @param array $configuration
     * @param string $plugin_id
     * @param mixed $plugin_definition
     *
     * @return static
     */
    public static function create(ContainerInterface $container, array $configuration, $plugin_id, $plugin_definition)
    {
        return new static(
            $configuration,
            $plugin_id,
            $plugin_definition,
            $container->get('current_user'),
            new MenuItems($container->get('menu.link_tree'))
        );
    }

    /**
     * {@inheritdoc}
     */
    public function build()
    {
        $items = $this->menuItems->getMenuTree('submenu');

        return [
            '#theme' => 'submenu',
            '#items' => $items,
            '#attached' => [
                'library' => [
                    'menu/submenu'
                ]
            ]
        ];
    }

    /**
     * Don't cache this page
     */
    public function getCacheMaxAge()
    {
        return 0;
    }
}
