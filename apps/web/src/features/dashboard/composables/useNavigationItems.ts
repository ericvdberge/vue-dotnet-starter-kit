import { Home, Lock } from "lucide-vue-next"
import type { NavigationGroup } from "../types/navigation-items"

export const useNavigationItems: () => NavigationGroup[] = () => {
    return [
        {
            title: 'Main',
            items: [
                {
                    title: 'Home',
                    icon: Home,
                    to: '/dashboard'
                }
            ]
        },
        {
            title: 'Management',
            items: [
                {
                    title: 'Access Control',
                    icon: Lock,
                    to: '/dashboard/access-control'
                }
            ]
        }
    ]
}