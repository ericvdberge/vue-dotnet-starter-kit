import { Home, Folder, Lock } from "lucide-vue-next"

export const useNavigationItems = () => {
    return [
        {
            title: 'Dashboard',
            icon: Home,
            to: '/dashboard'
        },
        {
            title: 'Projects',
            icon: Folder, 
            to: '/dashboard/projects'
        },
        {
            title: 'Access Control',
            icon: Lock,
            to: '/dashboard/access-control'
        }
    ]
}